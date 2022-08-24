import React, { Component } from "react";
import { Navigate } from "react-router-dom";
import AuthService from "../../services/auth.service";

export default class SignInCallback extends Component {
  constructor(props) {
    super(props);
    this.state = { isLoading: true, user: undefined };
  }

  componentDidMount() {
    this.getParamsFromRedirect();
  }

  async getParamsFromRedirect() {
    const urlParams = new URLSearchParams(window.location.search);
    let params = Object.fromEntries(urlParams);
    let { code } = params;
    if (code) {
      AuthService.confirmRedirect(params);
      await AuthService.getUserData().then((response) => {
        if (response.given_name) {
          this.setState({ isLoading: false, user: response });
        }
      });
    } else {
      throw new Error("Redirect params not found.");
    }
  }

  render() {
    let { user, isLoading } = this.state;
    return (
      <div>
        {isLoading ? (
          <div>PLease wait. Logging in... </div>
        ) : user ? (
          <Navigate to="/Home" />
        ) : (
          <div>Error</div>
        )}
      </div>
    );
  }
}
