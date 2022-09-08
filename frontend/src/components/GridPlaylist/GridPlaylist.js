import React from "react";
import InfiniteScroll from "react-infinite-scroll-component";
import GridItem from "../GridItem/GridItem";

export default class GridPlaylist extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      items: [],
      pageNumber: 1,
      page: props.page,
      hasMore: true,
    };
  }

  componentDidMount() {
    this.fetchMoreItems();
  }
  componentDidUpdate(prevProps) {
    if (this.props.page !== prevProps.page) {
      this.setState(
        {
          items: [],
          pageNumber: 1,
          page: this.props.page,
          hasMore: true,
        },
        () => this.fetchMoreItems()
      );
    }
  }

  render() {
    return (
      <div className="container body-content">
        <div className="col-md-12 h-100 jumbotron">
          <InfiniteScroll
            dataLength={this.state.items.length}
            next={this.fetchMoreItems.bind(this)}
            hasMore={this.state.hasMore}
            loader={
              <div className="spinner-border m-5" role="status">
                <span className="sr-only" />
              </div>
            }
          >
            {this.state.items.map((items, i) => (
              <GridItem gridItemData={items} key={`track${i}`} />
            ))}
          </InfiniteScroll>
        </div>
      </div>
    );
  }

  fetchMoreItems() {
    let { pageNumber, items } = this.state;
    this.props.next(pageNumber).then((response) => {
      this.setState({
        items: [...items, ...response.items],
        hasMore: response.paging.HasNext,
        pageNumber: this.state.pageNumber + 1,
      });
    });
  }
}
