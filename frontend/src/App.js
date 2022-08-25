import React, { Component } from "react";
import { Route, Routes, Link } from "react-router-dom";
import "./Site.css";

import AuthService from "./services/auth.service";

import SignInCallback from "./components/SignInCallback/SignInCallback";
import Login from "./components/Login/Login";
import Register from "./components/Register/Register";
import Playlists from "./components/Playlists/Playlists";
import WrappedPlaylist from "./components/Playlist/WrappedPlaylist";
import WrappedTrack from "./components/Track/WrappedTrack";
import Home from "./components/Home/Home";
import Uploads from "./components/Uploads/Uploads";
import NotFound from "./components/NotFound/NotFound";

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
    super(props);
    this.state = { isLoading: false };
    AuthService.finishedLoading = () => this.finishedLoading();
  }

  logOut(params) {
    AuthService.logout();
    window.location.href = params.returnTo;
  }

  logIn() {
    AuthService.login();
  }

  finishedLoading() {
    this.setState({ isLoading: false });
  }

  render() {
    var pages = ["Home", "Playlists", "Uploads"];
    let { isLoading, isLoggedIn, user } = AuthService;
    /*if (isLoading) {
      return <div>Loading user... Please wait.{isLoading}</div>;
    }*/
    /*
    if (error) {
      return <div>Oops... {error.message}</div>;
    }*/
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
                {isLoggedIn ? (
                  <div className="navbar-nav ml-auto">
                    <li className="nav-item">
                      <Link to={"/profile"} className="nav-link">
                        {user?.given_name}
                      </Link>
                    </li>
                    <li className="nav-item">
                      <Link
                        to=""
                        className="nav-link"
                        onClick={() =>
                          this.logOut({ returnTo: window.location.origin })
                        }
                      >
                        LogOut
                      </Link>
                    </li>
                  </div>
                ) : (
                  <div className="navbar-nav ml-auto">
                    <li className="nav-item">
                      <Link to="" className="nav-link" onClick={this.logIn}>
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
          <Route exact path="/signin-callback" element={<SignInCallback />} />
          <Route exact path="/Login" element={<Login />} />
          <Route exact path="/Register" element={<Register />} />
          <Route path="Uploads" element={<Uploads />} />
          <Route path="Playlists" element={<Playlists />} />
          <Route path="Playlists/:playlistId" element={<WrappedPlaylist />} />
          <Route path="track/:id" element={<WrappedTrack />} />
          <Route path=":user/:id" element={<WrappedTrack />} />
          <Route path="*" element={<NotFound />} />
          <Route path="not-found" element={<NotFound />} />
        </Routes>
      </div>
    );
  }
}
