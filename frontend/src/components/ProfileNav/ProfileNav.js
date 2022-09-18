import React from "react";
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
      <nav className="navbar navbar-expand navbar-light bg-light">
        <ul className="navbar-nav">
          {Object.keys(pages).map((page, i) => {
            return (
              <li className="nav-item" key={i}>
                <NavLink
                  to={pages[page]}
                  end={pages[page] === ""}
                  className="nav-link"
                >
                  {page}
                </NavLink>
              </li>
            );
          })}
        </ul>
      </nav>
    );
  }
}
