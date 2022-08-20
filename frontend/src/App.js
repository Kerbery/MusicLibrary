import React, { Component } from "react";
import { Route, Routes, Link } from "react-router-dom";
import "./Site.css";

import AuthService from "./services/auth.service";

import Login from "./components/Login/Login";
import Register from "./components/Register/Register";
import PLaylists from "./components/Playlists/Playlists";
import WrappedPlaylist from "./components/Playlist/WrappedPlaylist";
import Home from "./components/Home/Home";
import Uploads from "./components/Uploads/Uploads";
import NotFound from "./components/NotFound/NotFound";

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
    super(props);
    this.logOut = this.logOut.bind(this);
    this.state = { currentUser: undefined };
  }

  componentDidMount() {
    const user = AuthService.getCurrentUser();
    if (user) {
      this.setState({
        currentUser: user,
      });
    }
  }

  logOut() {
    AuthService.logout();
  }

  render() {
    const { currentUser } = this.state;
    var pages = ["Home", "Playlists", "Uploads"];

    return (
      <div>
        <nav className="navbar navbar-dark bg-dark justify-content-between">
          <div className="container">
            <a className="navbar-brand" href="/">
              MusicLibrary
            </a>
            <div className="navbar-expand">
              <ul className="nav navbar-nav">
                {pages.map((category, i) => (
                  <li className="nav-item" key={`nav${i}`}>
                    <Link className="nav-link" to={`/${category}`}>
                      {category}
                    </Link>
                  </li>
                ))}
                {currentUser ? (
                  <div className="navbar-nav ml-auto">
                    <li className="nav-item">
                      <Link to={"/profile"} className="nav-link">
                        {currentUser.username}
                      </Link>
                    </li>
                    <li className="nav-item">
                      <a
                        href="/login"
                        className="nav-link"
                        onClick={this.logOut}
                      >
                        LogOut
                      </a>
                    </li>
                  </div>
                ) : (
                  <div className="navbar-nav ml-auto">
                    <li className="nav-item">
                      <Link to={"/Login"} className="nav-link">
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

        <Routes>
          <Route exact path="/" element={<Home />} />
          <Route exact path="/Home" element={<Home />} />
          <Route exact path="/Login" element={<Login />} />
          <Route exact path="/Register" element={<Register />} />
          <Route path="Playlists" element={<PLaylists />} />
          <Route path="Playlists/:playlistId" element={<WrappedPlaylist />} />
          <Route path="Uploads" element={<Uploads />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </div>
    );
  }
}
