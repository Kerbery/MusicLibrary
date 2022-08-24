const webpack = require("webpack");

module.exports = {
  plugins: [
    new webpack.ProvidePlugin({
      process: "process/browser",
    }),
  ],
  resolve: {
    fallback: {
      buffer: require.resolve("buffer/"),
      stream: require.resolve("readable-stream"),
    },
  },
};
