import React, { Component } from "react";

export default class Home extends Component {
  render() {
    return (
      <div className="container mt-4">
        <div className="jumbotron align-middle">
          <h1 className="display-4">Welcome!</h1>
          <p className="lead">
            You are currently using the client app of the MusicLibrary. This app
            was created using Create-React-App.
          </p>
          <hr className="my-4" />
          <p>You can access the whole project's repository on GitHub.</p>
          <p className="lead">
            <a
              className="btn btn-primary btn-lg"
              href="https://github.com/Kerbery/MusicLibrary"
              role="button"
            >
              Go to GitHub
            </a>
          </p>
        </div>
      </div>
    );
  }
}
