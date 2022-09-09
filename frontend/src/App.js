import React, { Component } from "react";
import { Route, Routes } from "react-router-dom";

import AuthService from "./services/auth.service";
import PlaylistService from "./services/playlists.service";
import TrackService from "./services/track.service";

import SignInCallback from "./components/SignInCallback/SignInCallback";
import Login from "./components/Login/Login";
import Register from "./components/Register/Register";
import WrappedProfile from "./components/Profile/WrappedProfile";
import WrappedGridPlaylist from "./components/GridPlaylist/WrappedGridPlaylist";
import WrappedPlaylist from "./components/Playlist/WrappedPlaylist";
import WrappedTrack from "./components/Track/WrappedTrack";
import Home from "./components/Home/Home";
import NotFound from "./components/NotFound/NotFound";
import Nav from "./components/Nav/Nav";

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
    let { isLoading, isLoggedIn, user } = AuthService;
    /*if (isLoading) {
      return <div>Loading user... Please wait.{isLoading}</div>;
    }*/
    /*
    if (error) {
      return <div>Oops... {error.message}</div>;
    }*/
    return (
      <div className="d-flex flex-column h-100">
        <Nav
          isLoggedIn={isLoggedIn}
          user={user}
          logIn={this.logIn}
          logOut={this.logOut}
        ></Nav>
        <main className="flex-shrink-0">
          <Routes>
            <Route exact path="/" element={<Home />} />
            <Route exact path="/Home" element={<Home />} />
            <Route exact path="/signin-callback" element={<SignInCallback />} />
            <Route exact path="/Login" element={<Login />} />
            <Route exact path="/Register" element={<Register />} />
            <Route
              path=":user/playlists/:playlistId"
              element={<WrappedPlaylist />}
            />

            <Route path=":user" element={<WrappedProfile page="profile" />}>
              <Route
                path="tracks"
                element={
                  <WrappedGridPlaylist
                    page="tracks"
                    next={TrackService.getTracks}
                  />
                }
              />
              <Route
                path="playlists"
                element={
                  <WrappedGridPlaylist
                    page="playlists"
                    next={PlaylistService.getPlaylists}
                  />
                }
              />
              <Route
                path="likes"
                element={
                  <WrappedGridPlaylist
                    page="likes"
                    next={PlaylistService.getLikes}
                  />
                }
              />
            </Route>

            <Route path=":user/:track" element={<WrappedTrack />} />
            <Route path="*" element={<NotFound />} />
            <Route path="not-found" element={<NotFound />} />
          </Routes>
        </main>
      </div>
    );
  }
}
