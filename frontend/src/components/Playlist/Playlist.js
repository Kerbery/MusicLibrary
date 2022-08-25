import React from "react";
import GridItem from "../GridItem/GridItem";
import PlaylistsService from "../../services/playlists.service";

export default class Playlist extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      playlist: {},
      loading: true,
      playlistId: this.props.playlistId,
    };
  }

  componentDidMount() {
    this.populatePlaylistData(this.state.playlistId);
  }
  render() {
    let contents = this.state.loading ? "Loading" : this.renderPlaylistItems();
    return contents;
  }

  renderPlaylistItems() {
    var gridItemsData = this.state.playlist.items.map((item) => {
      var gridItemData = {};
      gridItemData.title = item.getTrackDTO.title;
      gridItemData.username = "Username";
      gridItemData.artworkUrl = item.getTrackDTO?.artworkUrl || "";
      gridItemData.url = `/Track/${item.getTrackDTO.urlId}`;
      return gridItemData;
    });

    return (
      <div className="container body-content">
        <h4 className="category_title">{this.state.playlist.title}</h4>
        <div className="col-md-12">
          {gridItemsData.map((gridItemData, i) => (
            <GridItem gridItemData={gridItemData} key={`track${i}`} />
          ))}
        </div>
      </div>
    );
  }

  populatePlaylistData(playlistId) {
    PlaylistsService.getPlaylistContent(playlistId).then(
      (response) => {
        this.setState({
          playlist: response.data,
          loading: false,
          playlistId: this.state.playlistId,
        });
      },
      (error) => {
        console.log(
          (error.response &&
            error.response.data &&
            error.response.data.message) ||
            error.message ||
            error.toString()
        );
      }
    );
  }
}
