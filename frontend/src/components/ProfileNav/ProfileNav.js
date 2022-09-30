import React from "react";
import { Navbar, Nav as NavRB } from "react-bootstrap";
import { NavLink } from "react-router-dom";
import "./ProfileNav.css";

export default class ProfileNav extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      pages: this.props.pages,
    };
  }
  render() {
    const { pages } = this.state;
    return (
      <Navbar bg="light" variant="light">
        <NavRB className="profileNavBar">
          {Object.keys(pages).map((page, i) => {
            return (
              <NavLink
                key={i}
                to={pages[page]}
                end={pages[page] === ""}
                className="nav-link"
              >
                {page}
              </NavLink>
            );
          })}
        </NavRB>
      </Navbar>
    );
  }
}
