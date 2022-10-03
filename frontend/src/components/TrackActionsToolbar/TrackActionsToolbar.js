import React from "react";
import PlaylistsService from "../../services/playlists.service";
import AuthService from "../../services/auth.service";
import AddToModal from "../AddToModal/AddToModal";
import "./TrackActionsToolbar.css";

export default class TrackActionsToolbar extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      trackId: props.trackId,
      isShowingAddToModal: false,
      isLikeLoading: false,
      isLiked: false,
      trackUrl: props.trackUrl,
    };

    this.handleLikeClick = this.handleLikeClick.bind(this);
    this.showAddToModal = this.showAddToModal.bind(this);
  }
  componentDidMount() {
    let username = AuthService.user.preferred_username;
    PlaylistsService.getAllLikedTracksIds(username).then((result) => {
      if (result.includes(this.state.trackId)) {
        this.setState({ isLiked: true });
      }
    });
  }

  handleLikeClick() {
    let { isLiked } = this.state;
    this.setState({ isLikeLoading: true }, () => {
      if (isLiked) {
        this.unlikeTrack();
      } else {
        this.likeTrack();
      }
    });
  }

  likeTrack() {
    PlaylistsService.likeTrack(this.state.trackId).then((response) => {
      this.setState({ isLiked: response, isLikeLoading: false });
    });
  }

  unlikeTrack() {
    PlaylistsService.unlikeTrack(this.state.trackId).then((response) => {
      this.setState({ isLiked: !response, isLikeLoading: false });
    });
  }

  showAddToModal() {
    this.setState({ isShowingAddToModal: true });
  }
  render() {
    let { trackId, trackUrl, isShowingAddToModal, isLiked, isLikeLoading } =
      this.state;
    let { className } = this.props;
    return (
      <div className={className}>
        <button
          title={isLiked ? "Unlike" : "Like"}
          className={"scButton me-1" + (isLiked ? " active" : "")}
          disabled={isLikeLoading ? isLikeLoading : undefined}
          onClick={!isLikeLoading ? this.handleLikeClick : null}
        >
          <span className="bi bi-heart-fill me-1 align-middle" />
          {isLiked ? "Liked" : "Like"}
        </button>
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
