import React, { Component } from "react";
import { Link } from "react-router-dom";
// import "./GridItem.css";
// import "./Thumbnails.css";

export default class GridItem extends Component {
  constructor(props) {
    super(props);
    this.state = { gridItemData: this.props.gridItemData };
  }
  render() {
    let backgroundImage = this.props.gridItemData.artworkUrl;
    backgroundImage = /http/.test(backgroundImage)
      ? `url(${backgroundImage})`
      : "";
    return (
      <div className="thumbnail col-md-2 col-sm-4">
        <div className="artwork">
          <Link to={this.state.gridItemData.url}>
            <span
              style={{ backgroundImage }}
              alt=""
              className="placeholder-art"
            />
          </Link>
        </div>
        <div className="description">
          <div className="track_title truncated">
            <Link to={this.state.gridItemData.url}>
              {this.state.gridItemData.title}
            </Link>
          </div>
          <div className="track_uploader truncated">
            <Link to={`/${this.state.gridItemData.user.permalink}/`}>
              {this.state.gridItemData.user.username}
            </Link>
          </div>
        </div>
      </div>
    );
  }
}
