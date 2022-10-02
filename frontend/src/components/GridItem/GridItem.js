import React, { Component } from "react";
import { Col } from "react-bootstrap";
import { Link } from "react-router-dom";
import LazyBackground from "../LazyBackground/LazyBackGround";
import "./GridItem.css";

export default class GridItem extends Component {
  constructor(props) {
    super(props);
    this.state = { gridItemData: this.props.gridItemData };
  }
  render() {
    let backgroundImage = this.props.gridItemData.artworkUrl;
    backgroundImage = /http/.test(backgroundImage) ? backgroundImage : "";
    return (
      <Col className="thumbnail" md="2" sm="4">
        <div className="artwork">
          <Link to={this.state.gridItemData.url}>
            <div className="placeholder-art">
              <LazyBackground
                src={backgroundImage}
                alt=""
                className="placeholder-art"
              />
            </div>
          </Link>
        </div>
        <div className="description">
          <div className="link-dark truncated">
            <Link to={this.state.gridItemData.url}>
              {this.state.gridItemData.title}
            </Link>
          </div>
          <div className="link-light truncated">
            <Link to={`/${this.state.gridItemData.user.permalink}`}>
              {this.state.gridItemData.user.username}
            </Link>
          </div>
        </div>
      </Col>
    );
  }
}
