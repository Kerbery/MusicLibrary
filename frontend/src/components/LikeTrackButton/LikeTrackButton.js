import React from "react";
import { connect } from "react-redux";
import { likeTrack, unlikeTrack, fetchLikesIds } from "../../redux/actions.js";
import { getLikeStatus } from "../../redux/selectors.js";
import authService from "../../services/auth.service.js";

class LikeTrackButton extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      trackId: props.trackId,
      isLiked: props.isLiked,
      isLikeLoading: false,
    };

    this.handleLikeClick = this.handleLikeClick.bind(this);
  }

  componentDidMount() {
    const username = authService.user.preferred_username;
    this.props.fetchLikesIds(username);
  }

  componentDidUpdate() {
    const { isLiked } = this.props;
    const { isLiked: wasLiked } = this.state;

    if (wasLiked !== isLiked) {
      this.setState({ isLiked, isLikeLoading: false });
    }
  }

  handleLikeClick() {
    let { isLiked, trackId } = this.state;

    this.setState({ isLikeLoading: true }, () => {
      if (isLiked) {
        this.props.unlikeTrack(trackId);
      } else {
        this.props.likeTrack(trackId);
      }
    });
  }

  render() {
    let { isLiked, isLikeLoading } = this.state;

    return (
      <button
        title={isLiked ? "Unlike" : "Like"}
        className={"scButton me-1" + (isLiked ? " active" : "")}
        disabled={isLikeLoading ? isLikeLoading : undefined}
        onClick={!isLikeLoading ? this.handleLikeClick : null}
      >
        <span className="bi bi-heart-fill me-1 align-middle" />
        {isLiked ? "Liked" : "Like"}
      </button>
    );
  }
}

const mapStateToProps = (state, props) => {
  const { trackId } = props;
  const { isLiked } = getLikeStatus(state, trackId);
  return { isLiked };
};

export default connect(mapStateToProps, {
  likeTrack,
  unlikeTrack,
  fetchLikesIds,
})(LikeTrackButton);
