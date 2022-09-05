import { Component } from "react";

export default class LazyBackground extends Component {
  state = { src: null };

  componentDidMount() {
    const { src } = this.props;

    const imageLoader = new Image();
    imageLoader.src = src;

    imageLoader.onload = () => {
      this.setState({ src });
    };
  }

  render() {
    let style = {};
    let className = "placeholder-art";
    if (this.state.src) {
      style = {
        backgroundImage: `url(${this.state.src})`,
        opacity: 1,
      };
      className = `${className} opacity-transition`;
    } else {
      style = { opacity: 0 };
    }
    return <span style={style} alt="" className={className} />;
  }
}
