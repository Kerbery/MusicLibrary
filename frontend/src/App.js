import React, { Component } from "react";
import { Route, Routes, Link } from "react-router-dom";
import "./Site.css";

import PLaylists from "./components/Playlists/Playlists";
import WrappedPlaylist from "./components/Playlist/WrappedPlaylist";
import Home from "./components/Home/Home";
import Uploads from "./components/Uploads/Uploads";
import NotFound from "./components/NotFound/NotFound";

export default class App extends Component {
  static displayName = App.name;

  constructor(props) {
    super(props);
    this.state = { playlists: [], loading: false };
  }

  static renderLayout(playlists) {
    var pages = ["Home", "Playlists", "Uploads"];
    return (
      <div>
        {/*<table className="table table-striped" aria-labelledby="tabelLabel">
          <thead>
            <tr>
              <th>Title</th>
              <th>Description</th>
              <th>Duration</th>
              <th>Uploaded</th>
            </tr>
          </thead>
          <tbody>
            {playlists.items.map(
              (track) => (
                <tr key={track.trackId}>
                  <td>{track.getTrackDTO.title}</td>
                  <td>{track.getTrackDTO.description}</td>
                  <td>
                    {Helpers.durationFromSeconds(track.getTrackDTO.duration)}
                  </td>
                  <td>{`${Helpers.timeSince(
                    track.getTrackDTO.uploadDate
                  )} ago`}</td>
                </tr>
              ),
              this
            )}
          </tbody>
                  </table>*/}
        <nav className="navbar navbar-dark bg-dark justify-content-between">
          <div className="container">
            <a className="navbar-brand" href="/">
              MusicLibrary
            </a>
            <div className="navbar-expand">
              <ul className="nav navbar-nav">
                {pages.map((category, i) => (
                  <li className="nav-item" key={`nav${i}`}>
                    {/* <a className="nav-link" href="#">
                        {category}
                      </a> */}
                    <Link className="nav-link" to={`/${category}`}>
                      {category}
                    </Link>
                  </li>
                ))}
                {/* @foreach (var category in ViewBag.ProfileCategories)
            {
                <li class="@(category == ViewBag.Category ? "active" : "")">
                    @Html.ActionLink((string) category, (string) category, "Profile")
                </li>
            } */}
              </ul>
            </div>
          </div>
        </nav>

        <Routes>
          <Route exact path="/" element={<Home />} />
          <Route path="Home" element={<Home />} />
          <Route path="Playlists" element={<PLaylists items={playlists} />} />
          <Route path="Playlists/:playlistId" element={<WrappedPlaylist />} />
          <Route path="Uploads" element={<Uploads />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
        {/* sd*/}
      </div>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>
          Loading... Please refresh once the ASP.NET backend has started. See{" "}
          <a href="https://aka.ms/jspsintegrationreact">
            https://aka.ms/jspsintegrationreact
          </a>{" "}
          for more details.
        </em>
      </p>
    ) : (
      App.renderLayout(this.state.playlists)
    );

    return (
      <div>
        {/* <h1 id="tabelLabel">Weather forecast</h1>
        <p>This component demonstrates fetching data from the server.</p> */}
        {contents}
      </div>
    );
  }
}
