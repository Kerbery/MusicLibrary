import React from "react";

export default class Spinner extends React.Component {
  render() {
    return (
      <div className="d-flex justify-content-center">
        <div className="spinner-border m-5" role="status">
          <span className="sr-only" />
        </div>
      </div>
    );
  }
}
