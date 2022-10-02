import React from "react";
import { Link } from "react-router-dom";
import { Col } from "react-bootstrap";
import Spinner from "../Spinner/Spinner";
import LazyBackground from "../LazyBackground/LazyBackGround";
import TrackService from "../../services/track.service";
import PlaylistsService from "../../services/playlists.service";
import AuthService from "../../services/auth.service";
import AddToModal from "../AddToModal/AddToModal";
import "./Track.css";

export default class Track extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      permalink: props.permalink,
      track: undefined,
      isLoading: true,
      isShowingAddToModal: false,
      isLikeLoading: false,
      isLiked: false,
      isFollowing: false,
      trackUrl: undefined,
    };

    this.handleFollowClick = this.handleFollowClick.bind(this);
    this.handleLikeClick = this.handleLikeClick.bind(this);
    this.showAddToModal = this.showAddToModal.bind(this);
  }

  componentDidMount() {
    this.populateTrackData();
  }

  populateTrackData() {
    TrackService.getTrackInfo(this.state.permalink).then((response) => {
      let track = response;
      let trackUrl = new URL(track.url, window.location.origin);
      this.setState({ isLoading: false, track, trackUrl }, () => {
        let username = AuthService.user.preferred_username;
        PlaylistsService.getAllLikedTracksIds(username).then((result) => {
          if (result.includes(this.state.track.id)) {
            this.setState({ isLiked: true });
          }
        });
      });
    });
  }

  handleFollowClick() {
    this.setState((state) => {
      return { isFollowing: !state.isFollowing };
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
    PlaylistsService.likeTrack(this.state.track.id).then((response) => {
      this.setState({ isLiked: response, isLikeLoading: false });
    });
  }

  unlikeTrack() {
    PlaylistsService.unlikeTrack(this.state.track.id).then((response) => {
      this.setState({ isLiked: !response, isLikeLoading: false });
    });
  }

  showAddToModal() {
    this.setState({ isShowingAddToModal: true });
  }

  render() {
    let { isLoading, isLiked, isLikeLoading, isFollowing, track } = this.state;

    return isLoading ? (
      <Spinner />
    ) : (
      <>
        {this.renderTrackHeader(track)}
        {this.renderTrackInfo(isLiked, isLikeLoading, isFollowing, track)}
      </>
    );
  }

  renderTrackHeader(track) {
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

  renderTrackInfo(isLiked, isLikeLoading, isFollowing, track) {
    return (
      <div className="px-3">
        <div className="my-2">
          <div className="buttonsGroup">
            <button
              title={isLiked ? "Unlike" : "Like"}
              className={"scButton me-1" + (isLiked ? " active" : "")}
              disabled={isLikeLoading ? isLikeLoading : undefined}
              onClick={!isLikeLoading ? this.handleLikeClick.bind(this) : null}
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
                navigator.clipboard.writeText(this.state.trackUrl);
              }}
            >
              <span className="bi bi-link me-1 align-middle" />
              Copy Link
            </button>
          </div>
          <AddToModal
            show={this.state.isShowingAddToModal}
            onShow={() => this.setState({ isShowingAddToModal: true })}
            onHide={() => this.setState({ isShowingAddToModal: false })}
            trackId={this.state.track.id}
          />
        </div>
        <div className="d-block position-relative">
          <div className="userBadge">
            <Link
              to={`/${track.user.permalink}`}
              className="badgeAvatar d-block"
            >
              <LazyBackground
                className="img-responsive placeholder-art"
                src={track.user.avatarUrl}
              ></LazyBackground>
            </Link>
            <div className="d-block my-2">
              <h2 className="link-dark truncated">
                <Link to={`/${track.user.permalink}`}>
                  {track.user.username}
                </Link>
              </h2>
            </div>

            <button
              title={isFollowing ? "Unfollow" : "Follow"}
              className={
                "scButton scFollowButton me-1" + (isFollowing ? " active" : "")
              }
              onClick={this.handleFollowClick}
            >
              <span
                className={
                  "bi me-1 align-middle" +
                  (isFollowing ? " bi-person-check-fill" : " bi-person-plus")
                }
              />
              {isFollowing ? "Following" : "Follow"}
            </button>
          </div>
          <div className="trackDescription">{track.description}</div>
        </div>
      </div>
    );
  }
}
