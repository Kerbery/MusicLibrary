import React from "react";
import { Link } from "react-router-dom";
import { Button, Modal, Nav, Tab, Form } from "react-bootstrap";
import InfiniteScroll from "react-infinite-scroll-component";
import LazyBackground from "../LazyBackground/LazyBackGround";
import PlaylistsService from "../../services/playlists.service";
import AuthService from "../../services/auth.service";
import Spinner from "../Spinner/Spinner";

import "./AddToModal.css";

export default class AddToModal extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      show: props.show ?? false,
      items: [],
      pageNumber: 1,
      hasMore: true,
      trackId: props.trackId,
      playlistTitle: "",
      createdPlaylistId: undefined,
    };
  }

  componentDidUpdate(prevProps) {
    let wasPreviouslyShowing = prevProps.show;
    let isCurrentlyShowing = this.props.show;

    if (!wasPreviouslyShowing && isCurrentlyShowing) {
      //showing modal
      this.setState(
        {
          show: isCurrentlyShowing,
          items: [],
          pageNumber: 1,
          hasMore: true,
        },
        () => this.fetchMoreItems()
      );
    } else if (wasPreviouslyShowing && !isCurrentlyShowing) {
      // closing modal
      this.setState({ playlistTitle: "", createdPlaylistId: undefined });
    }
  }

  fetchMoreItems() {
    console.log("looking for more items");
    let { pageNumber, items } = this.state;
    let userName = AuthService.user.preferred_username;
    PlaylistsService.getPlaylists(userName, pageNumber).then((response) => {
      this.setState({
        items: [...items, ...response.items],
        hasMore: response.paging.HasNext,
        pageNumber: pageNumber + 1,
      });
    });
  }

  handleClose() {
    this.setState({ show: false });
    this.props.onHide();
  }

  handleShow() {
    this.setState({ show: true });
    this.props.onShow();
  }

  handleInputChanged(event) {
    this.setState({
      playlistTitle: event.target.value,
    });
  }

  async addToPlaylist(playlistId) {
    const response = await PlaylistsService.addTrackToPlaylist(
      this.state.trackId,
      playlistId
    );
    this.setState(
      {
        items: [],
        pageNumber: 1,
        hasMore: true,
      },
      () => this.fetchMoreItems()
    );
    return response;
  }

  createPlaylist(event) {
    event.preventDefault();
    let { playlistTitle } = this.state;

    PlaylistsService.createPlaylist(playlistTitle).then((response) => {
      let { playlistId } = response;
      if (playlistId) {
        this.addToPlaylist(playlistId, this.state.trackId).then((response) => {
          if (response === true) {
            this.setState({ createdPlaylistId: playlistId });
          }
        });
      }
    });
  }

  removeFromPlaylist(trackId, playlistId) {
    let playlistItemId = this.state.items
      .find((pl) => pl.id === playlistId)
      .items.find((pi) => pi.trackId === trackId).id;

    PlaylistsService.deletePlaylistItem(playlistItemId).then(() =>
      this.setState(
        {
          items: [],
          pageNumber: 1,
          hasMore: true,
        },
        () => this.fetchMoreItems()
      )
    );
    /*.then(() => {
      let { items } = this.state;
      debugger;
      let newItems = items.map((pl) => {
        if (pl.id !== playlistId) {
          return pl;
        }
        let { items: oldItems, artworkUrl, ...rest } = pl;
        oldItems = oldItems.filter((pi) => pi.id !== playlistItemId);
        artworkUrl = oldItems[0].getTrackDTO?.artworkUrl ?? "";
        debugger;
        return { ...rest, artworkUrl, items: oldItems };
      });
      debugger;
      this.setState({ items: newItems });
    });*/
  }

  render() {
    let { items, hasMore, show, trackId, createdPlaylistId } = this.state;

    return (
      <Modal show={show} onHide={this.handleClose.bind(this)}>
        <Tab.Container id="addTo-tabs" defaultActiveKey="AddTo">
          <Modal.Header closeButton>
            <Nav variant="tabs">
              <Nav.Item>
                <Nav.Link eventKey="AddTo">Add to playlist</Nav.Link>
              </Nav.Item>
              <Nav.Item>
                <Nav.Link eventKey="Create">Create a playlist</Nav.Link>
              </Nav.Item>
            </Nav>
          </Modal.Header>
          <Modal.Body>
            <Tab.Content>
              <Tab.Pane eventKey="AddTo">
                <InfiniteScroll
                  dataLength={items.length}
                  next={this.fetchMoreItems.bind(this)}
                  hasMore={hasMore}
                  height={400}
                  loader={<Spinner />}
                >
                  {items.map((playlist, i) => (
                    <div key={i} className="addToPlaylistItem">
                      <Link to={playlist.url}>
                        <LazyBackground
                          className="addToPlaylistItemCover"
                          src={playlist.artworkUrl}
                        />
                      </Link>
                      <div className="addToPlaylistItemInfo">
                        <div className="link-dark truncated addToPlaylistItemTitle">
                          <Link to={playlist.url}>{playlist.title}</Link>
                        </div>
                        <span className="bi bi-music-note">
                          {playlist.items.length}
                        </span>
                      </div>

                      {playlist.items.some((pi) => pi.trackId === trackId) ? (
                        <Button
                          className="addedToPlaylistItem"
                          variant="warning"
                          onClick={() =>
                            this.removeFromPlaylist(trackId, playlist.id)
                          }
                        >
                          Added
                        </Button>
                      ) : (
                        <Button
                          className="addToPlaylistItem"
                          onClick={() => this.addToPlaylist(playlist.id)}
                        >
                          Add to playlist
                        </Button>
                      )}
                    </div>
                  ))}
                </InfiniteScroll>
              </Tab.Pane>
              <Tab.Pane eventKey="Create">
                {createdPlaylistId ? (
                  <Link to={`/ted/playlists/${createdPlaylistId}`}>
                    Open playlist
                  </Link>
                ) : (
                  <Form onSubmit={this.createPlaylist.bind(this)}>
                    <Form.Group className="mb-3" controlId="playlistTitle">
                      <Form.Label>Playlist title:</Form.Label>
                      <Form.Control
                        required
                        value={this.state.playlistTitle}
                        onChange={this.handleInputChanged.bind(this)}
                      />
                    </Form.Group>
                    <Button type="submit" variant="primary">
                      Save Changes
                    </Button>
                  </Form>
                )}
              </Tab.Pane>
            </Tab.Content>
          </Modal.Body>
        </Tab.Container>
      </Modal>
    );
  }
}
