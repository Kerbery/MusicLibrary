import React from "react";
import { Link } from "react-router-dom";
import LazyBackground from "../LazyBackground/LazyBackGround";
import "./TrackUploaderBadge.css";

export default class TrackUploaderBadge extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      user: props.user,
      isFollowing: false,
    };

    this.handleFollowClick = this.handleFollowClick.bind(this);
  }

  handleFollowClick() {
    this.setState((state) => ({ isFollowing: !state.isFollowing }));
  }

  render() {
    let { user, isFollowing } = this.state;
    return (
      <div className="userBadge">
        <Link to={`/${user.permalink}`} className="badgeAvatar d-block">
          <LazyBackground
            className="img-responsive placeholder-art"
            src={user.avatarUrl}
          />
        </Link>
        <div className="d-block my-2">
          <h2 className="link-dark truncated">
            <Link to={`/${user.permalink}`}>{user.username}</Link>
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
    );
  }
}
