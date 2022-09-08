const { createProxyMiddleware } = require("http-proxy-middleware");

const context = [
  "/gateway/Playlists",
  "/gateway/PlaylistItems",
  "/gateway/LikedTracks",
  "/gateway/Tracks",
];

module.exports = function (app) {
  const appProxy = createProxyMiddleware(context, {
    target: "https://localhost:5003",
    secure: false,
  });

  app.use(appProxy);
};
