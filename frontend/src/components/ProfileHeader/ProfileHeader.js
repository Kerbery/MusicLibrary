import React from "react";
import LazyBackground from "../LazyBackground/LazyBackGround";
import "./ProfileHeader.css";

export default class ProfileHeader extends React.Component {
  constructor(props) {
    super(props);
    this.state = { user: props.user };
  }
  render() {
    let { user } = this.state;
    let backgroundImage = user?.avatarUrl || "";
    return (
      <header className="profileHeader d-flex">
        <div className="pe-4">
          <LazyBackground
            src={backgroundImage}
            alt=""
            className="profileHeaderAvatar"
          />
        </div>
        <div className="align-self-start p-2">
          <h2 className="profileTitle">{user.userName}</h2>
          <br />
          {user.firstName && (
            <h3 className="profileAdditionalInfo">
              {`${user.firstName} ${user.lastName}`}
            </h3>
          )}
        </div>
      </header>
    );
  }
}
