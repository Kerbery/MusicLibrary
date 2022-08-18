import React from "react";
import GridItem from "../GridItem/GridItem";

export default class PLaylists extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      playlists: [],
      loading: true,
    };
  }

  componentDidMount() {
    this.populatePLaylistsData();
  }
  render() {
    let contents = this.state.loading ? "Loading" : this.renderPLaylistItems();
    return contents;
  }
  renderPLaylistItems() {
    console.log(this.state.playlists);
    var gridItemsData = this.state.playlists.map((playlist) => {
      var gridItemData = {};
      gridItemData.title = playlist.title;
      gridItemData.username = "Username";
      gridItemData.artworkUrl =
        playlist.items?.[0]?.getTrackDTO?.artworkUrl || "";
      gridItemData.url = `/Playlists/${playlist.playlistId}`;
      return gridItemData;
    });

    return (
      <div className="container body-content">
        <h4 className="category_title">Playlists</h4>
        <div className="col-md-12">
          {gridItemsData.map((gridItemData, i) => (
            // <div className="list_item" key={`track${i}`}>
            <GridItem gridItemData={gridItemData} key={`track${i}`} />
            // </div>
          ))}
        </div>
      </div>
    );
  }

  async populatePLaylistsData() {
    const response = await fetch("api/Playlists");
    const data = await response.json();
    console.log(data);
    this.setState({ playlists: data, loading: false });
  }
}
