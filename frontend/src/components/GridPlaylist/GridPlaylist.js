import React from "react";
import { Col, Container } from "react-bootstrap";
import InfiniteScroll from "react-infinite-scroll-component";
import GridItem from "../GridItem/GridItem";
import Spinner from "../Spinner/Spinner";

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
    let previousPage = prevProps.page;
    let currentPage = this.props.page;
    if (currentPage !== previousPage) {
      this.setState(
        {
          items: [],
          pageNumber: 1,
          page: currentPage,
          hasMore: true,
        },
        () => this.fetchMoreItems()
      );
    }
  }

  render() {
    return (
      <Container className="body-content">
        <Col md="12" className="h-100">
          <InfiniteScroll
            dataLength={this.state.items.length}
            next={this.fetchMoreItems.bind(this)}
            hasMore={this.state.hasMore}
            loader={<Spinner />}
          >
            {this.state.items.map((item, i) => (
              <GridItem gridItemData={item} key={`track${i}`} />
            ))}
          </InfiniteScroll>
        </Col>
      </Container>
    );
  }

  fetchMoreItems() {
    let { pageNumber, items } = this.state;
    this.props.next(pageNumber).then((response) => {
      this.setState({
        items: [...items, ...response.items],
        hasMore: response.paging.HasNext,
        pageNumber: pageNumber + 1,
      });
    });
  }
}
