import React from "react";
import InfiniteScroll from "react-infinite-scroll-component";
import GridItem from "../GridItem/GridItem";
import PlaylistsService from "../../services/playlists.service";

export default class Playlist extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      playlist: { title: "...", description: "" },
      playlistItems: [],
      loading: true,
      playlistId: this.props.playlistId,
      pageNumber: 1,
    };
  }

  componentDidMount() {
    PlaylistsService.getPlaylist(this.state.playlistId)
      .then((response) => {
        this.setState({
          playlist: {
            title: response.data.title,
            description: response.data.description,
          },
        });
      })
      .then(() => this.fetchPlaylistItems());
  }

  render() {
    let contents = this.state.loading ? "Loading" : this.renderPlaylistItems();
    return contents;
  }

  renderPlaylistItems() {
    var gridItemsData = this.state.playlistItems.map((item) => {
      var gridItemData = {};
      gridItemData.title = item.getTrackDTO.title;
      gridItemData.user = {
        username: item.getTrackDTO.user.userName,
        permalink: item.getTrackDTO.user.permalink,
      };

      gridItemData.artworkUrl = item.getTrackDTO?.artworkUrl || "";
      gridItemData.url = `/${item.getTrackDTO.user.permalink}/${item.getTrackDTO.permalink}`;
      return gridItemData;
    });

    return (
      <div className="container body-content">
        <h4 className="category_title">{this.state.playlist.title}</h4>
        <div className="col-md-12">
          <InfiniteScroll
            dataLength={this.state.playlistItems.length}
            next={this.fetchPlaylistItems.bind(this)}
            hasMore={this.state.hasMore}
            loader={<h4>Loading...</h4>}
          >
            {gridItemsData.map((gridItemData, i) => (
              <GridItem gridItemData={gridItemData} key={`track${i}`} />
            ))}
          </InfiniteScroll>
        </div>
      </div>
    );
  }

  fetchPlaylistItems() {
    let { playlistId, pageNumber } = this.state;
    PlaylistsService.getPlaylistItems(playlistId, pageNumber).then(
      (response) => {
        let pagingMetadata = JSON.parse(response.headers["x-pagination"]);
        this.setState({
          playlistItems: [...this.state.playlistItems, ...response.data],
          loading: false,
          hasMore: pagingMetadata.HasNext,
          pageNumber: this.state.pageNumber + 1,
        });
      },
      (error) => console.log(error)
    );
  }
}
