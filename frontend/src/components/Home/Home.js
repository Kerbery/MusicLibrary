import React, { Component } from "react";
import { Button, Container } from "react-bootstrap";

export default class Home extends Component {
  render() {
    return (
      <Container className="mt-4">
        <div className="bg-light p-5 rounded-3 m-3 align-middle">
          <h1 className="display-4">Welcome!</h1>
          <p className="lead">
            You are currently using the client app of the MusicLibrary. This app
            was created using Create-React-App.
          </p>
          <hr className="my-4" />
          <p>You can access the whole project's repository on GitHub.</p>
          <p className="lead">
            <Button
              variant="primary"
              size="lg"
              href="https://github.com/Kerbery/MusicLibrary"
            >
              Go to GitHub
            </Button>
          </p>
        </div>
      </Container>
    );
  }
}
