import React from "react";
import { NavLink, Outlet } from "react-router-dom";
import LazyBackground from "../LazyBackground/LazyBackGround";
import UserService from "../../services/user.service";
import "./Profile.css";

export default class Profile extends React.Component {
  constructor(props) {
    super(props);
    console.log(props);
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

  fetchUserInfo(userId) {
    UserService.getUserInfo(userId).then((user) => {
      this.setState({ user, isLoading: false });
    });
  }

  componentDidUpdate(prevProps) {
    if (prevProps.user !== this.props.user) {
      this.setState({ userId: this.props.user, isLoading: true }, () => {
        this.fetchUserInfo(this.state.userId);
      });
    }
  }

  render() {
    const { userId, isLoading } = this.state;
    const pages = {
      Profile: "",
      Tracks: "/tracks",
      Likes: "/likes",
      Playlists: "/playlists",
    };
    let backgroundImage = this.state.user?.avatarUrl || "";
    if (isLoading) {
      return <div className="container" />;
    }
    return (
      <div className="container">
        <header className="profileHeader d-flex">
          <LazyBackground
            src={backgroundImage}
            alt=""
            className="profileHeaderAvatar"
          />
          <div className="align-self-start p-2">
            <h2 className="profileTitle">{this.state.user.userName}</h2>
            <br />
            {this.state.user.firstName && (
              <h3 className="profileAdditionalInfo">
                {`${this.state.user.firstName} ${this.state.user.lastName}`}
              </h3>
            )}
          </div>
        </header>

        <nav className="navbar navbar-expand navbar-light bg-light">
          <ul className="navbar-nav">
            {Object.keys(pages).map((page, i) => {
              return (
                <li className="nav-item" key={i}>
                  <NavLink
                    to={`/${userId}${pages[page]}`}
                    end={pages[page] === ""}
                    className="nav-link"
                  >
                    {page}
                  </NavLink>
                </li>
              );
            })}
          </ul>
        </nav>
        <Outlet />
      </div>
    );
  }
}
