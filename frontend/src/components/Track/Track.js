import React from "react";
import Spinner from "../Spinner/Spinner";
import TrackService from "../../services/track.service";
import TrackHeader from "../TrackHeader/TrackHeader";
import TrackActionsToolbar from "../TrackActionsToolbar/TrackActionsToolbar";
import TrackUploaderBadge from "../TrackUploaderBadge/TrackUploaderBadge";
import "./Track.css";

export default class Track extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      permalink: props.permalink,
      track: undefined,
      isLoading: true,
      trackUrl: undefined,
    };
  }

  componentDidMount() {
    this.populateTrackData();
  }

  populateTrackData() {
    TrackService.getTrackInfo(this.state.permalink).then((response) => {
      let track = response;
      let trackUrl = new URL(track.url, window.location.origin);
      this.setState({ isLoading: false, track, trackUrl });
    });
  }

  render() {
    let { isLoading, track, trackUrl } = this.state;

    return isLoading ? (
      <Spinner />
    ) : (
      <>
        <TrackHeader track={track} />
        <div className="px-3">
          <TrackActionsToolbar
            className="my-2 buttonsGroup"
            trackId={track.id}
            trackUrl={trackUrl}
          />
          <div className="d-block position-relative">
            <div className="trackDescription">{track.description}</div>
            <TrackUploaderBadge user={track.user} />
          </div>
        </div>
      </>
    );
  }
}
