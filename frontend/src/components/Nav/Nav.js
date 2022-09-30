import React from "react";
import { Navbar, Nav as NavRB, NavDropdown, Container } from "react-bootstrap";
import { LinkContainer } from "react-router-bootstrap";
import AuthService from "../../services/auth.service";
import "./Nav.css";

export default class Nav extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  render() {
    let { isLoggedIn, logIn } = this.props;

    return (
      <Navbar bg="dark" variant="dark" fixed="top">
        <Container>
          <Navbar.Brand href="/">MusicLibrary</Navbar.Brand>
          <NavRB>
            {isLoggedIn ? (
              this.renderUserDropdown()
            ) : (
              <>
                <NavRB.Link onClick={logIn}>Login</NavRB.Link>
                <NavRB.Link href="/Register">Sign Up</NavRB.Link>
              </>
            )}
          </NavRB>
        </Container>
      </Navbar>
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
    let userName = AuthService.user.preferred_username;

    return (
      <NavDropdown
        id="collasible-nav-dropdown"
        title={
          <>
            <span className="img-circle avatar" alt="&nbsp;" />
            {` ${user.given_name}`}
          </>
        }
      >
        {Object.keys(pages).map((category, i) => (
          <LinkContainer
            key={i}
            isActive={() => false}
            to={`/${userName}${pages[category]}`}
          >
            <NavDropdown.Item>{category}</NavDropdown.Item>
          </LinkContainer>
        ))}
        <NavDropdown.Divider />
        <NavDropdown.Item
          onClick={() => logOut({ returnTo: window.location.origin })}
        >
          LogOut
        </NavDropdown.Item>
      </NavDropdown>
    );
  }
}
