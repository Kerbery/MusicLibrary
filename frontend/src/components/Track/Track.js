import React from "react";
import TrackService from "../../services/track.service";
import { Link } from "react-router-dom";

export default class Track extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      permalink: this.props.permalink,
      track: undefined,
      isLoading: true,
    };
  }
  componentDidMount() {
    this.populateTrackData();
  }

  populateTrackData() {
    TrackService.getTrackInfo(this.state.permalink).then((response) => {
      this.setState({ track: response.data, isLoading: false });
    });
  }

  render() {
    let { isLoading, permalink } = this.state;

    return isLoading ? `Loading "${permalink}"...` : this.renderTrackPage();
  }

  renderTrackPage() {
    let { track } = this.state;
    let backgroundImage = track.artworkUrl;
    backgroundImage = /http/.test(backgroundImage)
      ? `url(${backgroundImage})`
      : "";
    let content = (
      <div className="container-xl" style={{ marginTop: "20px" }}>
        <div className="row">
          <div className="col-4 col-md-4 col-sm-4">
            <div className="artwork">
              <span
                className="img-responsive placeholder-art"
                style={{ backgroundImage }}
                alt={track.title}
              />
            </div>
          </div>
          <div className="col-8 col-md-8 col-sm-8" trackid={track.id}>
            <div className="track_uploader truncated col-md-12">
              <Link to="#">Uploader</Link>
            </div>
            <div className="col-md-12">{track.title}</div>
            <div className="col-md-12">{track.description}</div>

            <div className="col-md-12">
              <button className="likeButton btn btn-secondary">
                {false ? (
                  <span className="glyphicon glyphicon-heart" title="Unlike">
                    Unlike
                  </span>
                ) : (
                  <span
                    className="glyphicon glyphicon-heart-empty"
                    title="Like"
                  >
                    Like
                  </span>
                )}
              </button>
              <button className="addToButton btn btn-secondary" title="Add to">
                <span className=" glyphicon glyphicon-plus-sign">Add to</span>
              </button>
            </div>

            <audio controls className="col-md-12">
              <source src={track.mediaUrl} type="audio/mpeg" />
              Your browser does not support the audio element.
            </audio>
          </div>
        </div>
      </div>
    );
    return content;
  }
}
