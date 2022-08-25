import React from "react";
import { Link } from "react-router-dom";

export default class Nav extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    var pages = ["Home", "Playlists", "Uploads"];
    let { isLoggedIn, user, logIn, logOut } = this.props;

    return (
      <nav className="navbar navbar-dark bg-dark justify-content-between">
        <div className="container">
          <a className="navbar-brand" href="/">
            MusicLibrary
          </a>
          <div className="navbar-expand">
            <ul className="nav navbar-nav">
              {isLoggedIn ? (
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
                    {pages.map((category, i) => (
                      <li key={i}>
                        <Link className=" dropdown-item" to={`/${category}`}>
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
                        onClick={() =>
                          logOut({ returnTo: window.location.origin })
                        }
                      >
                        LogOut
                      </Link>
                    </li>
                  </ul>
                </li>
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
}
