import axios from "axios";
import AuthService from "./auth.service";
import { getTrackDataFromItem, getPlaylistData } from "./mapping.helpers";

const PLAYLISTS_API_URL = "/gateway/Playlists";
const PLAYLIST_ITEMS_API_URL = "/gateway/PlaylistItems";
const LIKED_TRACKS_API_URL = "/gateway/LikedTracks";

class PlaylistService {
  async getPlaylists(userPermalink, PageNumber = 1, PageSize = 24) {
    let params = new URLSearchParams({
      userPermalink,
      PageNumber,
      PageSize,
    });
    return axios.get(`${PLAYLISTS_API_URL}?${params}`).then(
      (response) => {
        let paging = JSON.parse(response.headers["x-pagination"]);
        let items = response.data.map((item) => getPlaylistData(item));
        return { paging, items };
      },
      (error) => console.log(error)
    );
  }

  async getPlaylist(playlistId) {
    let token = await AuthService.getAccessToken();
    return axios.get(`${PLAYLISTS_API_URL}/${playlistId}`, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }

  async getPlaylistItems(playlistId, PageNumber = 1, PageSize = 24) {
    let token = await AuthService.getAccessToken();
    let params = new URLSearchParams({
      playlistId,
      PageNumber,
      PageSize,
    });
    const path = `${PLAYLIST_ITEMS_API_URL}?${params}`;
    return axios.get(path, {
      headers: { Authorization: `Bearer ${token}` },
    });
  }

  async getLikes(userPermalink, PageNumber = 1, PageSize = 24) {
    let params = new URLSearchParams({
      userPermalink,
      PageNumber,
      PageSize,
    });
    return axios.get(`${LIKED_TRACKS_API_URL}?${params}`).then(
      (response) => {
        let paging = JSON.parse(response.headers["x-pagination"]);
        let items = response.data.map((item) => getTrackDataFromItem(item));

        return { paging, items };
      },
      (error) => console.log(error)
    );
  }

  async getLikedTracksIds(userPermalink, PageNumber, PageSize) {
    let token = await AuthService.getAccessToken();
    let params = new URLSearchParams({
      userPermalink,
      PageNumber,
      PageSize,
    });
    return axios
      .get(`${LIKED_TRACKS_API_URL}/Ids?${params}`, {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then(
        (response) => {
          let paging = JSON.parse(response.headers["x-pagination"] || {});
          let ids = response.data;
          return { paging, ids };
        },
        (error) => {
          debugger;
          console.log(error);
          if (error.response.status === 401) {
            //AuthService.login();
          }
        }
      );
  }

  async getAllLikedTracksIds(userPermalink) {
    let ids = [];
    let PageNumber = 1;
    let PageSize = 200;

    while (true) {
      const result = await this.getLikedTracksIds(
        userPermalink,
        PageNumber,
        PageSize
      ).catch((error) => {
        debugger;
        console.log(error);
      });

      ids = [...ids, ...result.ids];

      if (!result.paging.HasNext) {
        break;
      }

      PageNumber += 1;
    }

    return ids;
  }

  async likeTrack(trackId) {
    let token = await AuthService.getAccessToken();
    return axios
      .put(
        `${LIKED_TRACKS_API_URL}/${trackId}`,
        {},
        {
          headers: { Authorization: `Bearer ${token}` },
        }
      )
      .then(
        (response) => {
          debugger;
          console.log(response);
          return response.status === 200;
        },
        (error) => {
          debugger;
          console.log(error);
          return error.response.status === 200;
        }
      );
  }

  async unlikeTrack(trackId) {
    let token = await AuthService.getAccessToken();
    return axios
      .delete(`${LIKED_TRACKS_API_URL}/${trackId}`, {
        headers: { Authorization: `Bearer ${token}` },
      })
      .then(
        (response) => {
          debugger;
          console.log(response);
          return response.status === 200;
        },
        (error) => {
          console.log(error);
          return error.response.status === 200;
        }
      );
  }
}
export default new PlaylistService();
