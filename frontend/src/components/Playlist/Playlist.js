import React from "react";
import GridItem from "../GridItem/GridItem";

export default class PLaylist extends React.Component {
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
    let contents = this.state.loading ? "Loading" : this.renderPLaylistItems();
    return contents;
  }

  renderPLaylistItems() {
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

  async populatePlaylistData(playlistId) {
    const response = await fetch(`/gateway/Playlists/${playlistId}`);
    const data = await response.json();
    console.log("Fetched data:", data);
    this.setState({
      playlist: data,
      loading: false,
      playlistId: this.state.playlistId,
    });
  }
}
