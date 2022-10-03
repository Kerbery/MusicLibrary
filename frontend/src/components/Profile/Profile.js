import React from "react";
import { Outlet } from "react-router-dom";
import { Container } from "react-bootstrap";
import UserService from "../../services/user.service";
import Spinner from "../Spinner/Spinner";
import ProfileHeader from "../ProfileHeader/ProfileHeader";
import ProfileNav from "../ProfileNav/ProfileNav";
import "./Profile.css";

export default class Profile extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      userId: props.user,
      page: props.page,
      user: {},
      isLoading: true,
    };
  }

  componentDidMount() {
    let { userId } = this.state;
    this.fetchUserInfo(userId);
  }

  componentDidUpdate(prevProps) {
    let previousUserId = prevProps.user;
    let currentUserId = this.props.user;

    if (currentUserId !== previousUserId) {
      this.setState({ isLoading: true }, () =>
        this.fetchUserInfo(currentUserId)
      );
    }
  }

  fetchUserInfo(userId) {
    UserService.getUserInfo(userId).then((user) => {
      this.setState({ user, userId, isLoading: false });
    });
  }

  render() {
    const { isLoading, user } = this.state;
    const pages = {
      Profile: "",
      Tracks: "tracks",
      Likes: "likes",
      Playlists: "playlists",
    };
    return (
      <Container className="px-0">
        {isLoading ? (
          <Spinner />
        ) : (
          <>
            <ProfileHeader user={user} />
            <ProfileNav pages={pages} />
            <Outlet />
          </>
        )}
      </Container>
    );
  }
}
