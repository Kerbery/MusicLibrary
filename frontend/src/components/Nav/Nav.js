import React from "react";
import { Link } from "react-router-dom";
import AuthService from "../../services/auth.service";

export default class Nav extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    let { isLoggedIn, logIn } = this.props;

    return (
      <nav className="navbar fixed-top navbar-dark bg-dark justify-content-between">
        <div className="container">
          <a className="navbar-brand" href="/">
            MusicLibrary
          </a>
          <div className="navbar-expand">
            <ul className="nav navbar-nav">
              {isLoggedIn ? (
                this.renderUserDropdown()
              ) : (
                <div className="navbar-nav ml-auto">
                  <li className="nav-item">
                    <Link to="" className="nav-link" onClick={logIn}>
                      Login
                    </Link>
                  </li>
                  <li className="nav-item">
                    <Link to={"/Register"} className="nav-link">
                      Sign Up
                    </Link>
                  </li>
                </div>
              )}
            </ul>
          </div>
        </div>
      </nav>
    );
  }

  renderUserDropdown() {
    const pages = {
      Profile: "",
      Tracks: "/tracks",
      Likes: "/likes",
      Playlists: "/playlists",
    };
    let { user, logOut } = this.props;
    let userId = AuthService.user.preferred_username;

    return (
      <li className="nav-item dropdown">
        <Link
          className="nav-link dropdown-toggle"
          to="#"
          role="button"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          <span className="img-circle avatar" alt="&nbsp;"></span>
          {` ${user.given_name} `}
        </Link>
        <ul className="dropdown-menu">
          {Object.keys(pages).map((category, i) => (
            <li key={i}>
              <Link
                className="dropdown-item"
                to={`/${userId}${pages[category]}`}
              >
                {category}
              </Link>
            </li>
          ))}
          <li>
            <hr className="dropdown-divider" />
          </li>
          <li>
            <Link
              to=""
              className="dropdown-item"
              onClick={() => logOut({ returnTo: window.location.origin })}
            >
              LogOut
            </Link>
          </li>
        </ul>
      </li>
    );
  }
}
