import React from "react";
import AddToModal from "../AddToModal/AddToModal";
import LikeTrackButton from "../LikeTrackButton/LikeTrackButton";
import "./TrackActionsToolbar.css";

export default class TrackActionsToolbar extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      trackId: props.trackId,
      isShowingAddToModal: false,
      trackUrl: props.trackUrl,
    };

    this.showAddToModal = this.showAddToModal.bind(this);
  }

  showAddToModal() {
    this.setState({ isShowingAddToModal: true });
  }

  render() {
    let { trackId, trackUrl, isShowingAddToModal } = this.state;
    let { className } = this.props;

    return (
      <div className={className}>
        <LikeTrackButton trackId={trackId} />
        <button
          title="Add to"
          className="scButton me-1"
          onClick={this.showAddToModal}
        >
          <span className="bi bi-music-note-list me-1 align-middle" />
          Add to
        </button>
        <button
          title="Copy link"
          className="scButton me-1"
          onClick={() => {
            navigator.clipboard.writeText(trackUrl);
          }}
        >
          <span className="bi bi-link me-1 align-middle" />
          Copy Link
        </button>
        <AddToModal
          show={isShowingAddToModal}
          onShow={() => this.setState({ isShowingAddToModal: true })}
          onHide={() => this.setState({ isShowingAddToModal: false })}
          trackId={trackId}
        />
      </div>
    );
  }
}
