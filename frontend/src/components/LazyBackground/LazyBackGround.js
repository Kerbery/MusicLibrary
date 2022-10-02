import { Component } from "react";
import "./LazyBackGround.css";

export default class LazyBackground extends Component {
  state = { loadedSrc: null };

  componentDidMount() {
    const { src } = this.props;
    const imageLoader = new Image();
    imageLoader.src = src;

    imageLoader.onload = () => {
      this.setState({ loadedSrc: src });
    };
  }

  componentDidUpdate(prevProps) {
    if (prevProps.src !== this.props.src) {
      this.componentDidMount();
    }
  }

  render() {
    let { className = "", src: propSrc, children } = this.props;
    let { loadedSrc } = this.state;
    let style = { opacity: 1 };

    className = `${className} sc-artwork`;
    if (loadedSrc) {
      style.backgroundImage = `url(${loadedSrc})`;
      className = `${className} opacity-transition`;
    } else if (propSrc) {
      style = { opacity: 0 };
    }

    return (
      <div className={className}>
        <span style={style} alt="" className={className}>
          {children}
        </span>
      </div>
    );
  }
}
