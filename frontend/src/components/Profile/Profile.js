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
      userId: this.props.user,
      page: this.props.page,
      user: {},
      isLoading: true,
    };
  }

  componentDidMount() {
    let { userId } = this.state;
    this.fetchUserInfo(userId);
  }

  componentDidUpdate(prevProps) {
    if (prevProps.user !== this.props.user) {
      this.setState({ userId: this.props.user, isLoading: true }, () => {
        this.fetchUserInfo(this.state.userId);
      });
    }
  }

  fetchUserInfo(userId) {
    UserService.getUserInfo(userId).then((user) => {
      this.setState({ user, isLoading: false });
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
