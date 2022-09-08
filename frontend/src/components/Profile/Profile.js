import React, { Component } from "react";
import { NavLink, Outlet } from "react-router-dom";

export default class Profile extends Component {
  constructor(props) {
    super(props);
    console.log(props);
    this.state = {
      userId: this.props.user,
      page: this.props.page,
    };
  }

  render() {
    const { userId } = this.state;
    const pages = {
      Profile: "",
      Tracks: "/tracks",
      Likes: "/likes",
      Playlists: "/playlists",
    };
    return (
      <div className="container">
        <header className=" profileHeader d-flex">
          <span className="profileHeaderAvatar"></span>
          <div className="align-self-start p-2">
            <h2 className="profileTitle">{userId}</h2>
          </div>
        </header>

        <nav className="navbar navbar-expand navbar-light bg-light">
          <ul className="navbar-nav">
            {Object.keys(pages).map((page, i) => {
              return (
                <li className="nav-item" key={i}>
                  <NavLink
                    to={`/${userId}${pages[page]}`}
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
        <Outlet />
      </div>
    );
  }
}
