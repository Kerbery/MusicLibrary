import React from "react";
import { Link } from "react-router-dom";
import { Col } from "react-bootstrap";
import LazyBackground from "../LazyBackground/LazyBackGround";
import "./TrackHeader.css";

export default class TrackHeader extends React.Component {
  render() {
    let { track } = this.props;
    return (
      <header className="d-flex trackHeader">
        <div className="d-flex flex-wrap flex-grow-1 flex-column align-items-start p-3">
          <div className="d-block mb-2">
            <h1 className="scFont trackTitle">{track.title}</h1>
          </div>
          <div className="d-block">
            <h2 className="scFont trackUploader truncated">
              <Link to={`/${track.user.permalink}`}>{track.user.username}</Link>
            </h2>
          </div>
          <Col as="audio" md="12" controls className="mt-auto">
            <source src={track.mediaUrl} type="audio/mpeg" />
            Your browser does not support the audio element.
          </Col>
        </div>
        <div className="trackArtwork flex-shrink-0 m-3">
          <LazyBackground
            className="img-responsive placeholder-art"
            src={track.artworkUrl}
          ></LazyBackground>
        </div>
      </header>
    );
  }
}
