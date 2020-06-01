'use strict';

const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const TerserPlugin = require('terser-webpack-plugin');
const OptimizeCSSAssetsPlugin = require('optimize-css-assets-webpack-plugin');
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;

module.exports = {
  resolve: {
    //extensions: [".ts", ".js", '.vue'],
    alias: {
      'vue$': 'vue/dist/vue.esm.js'
    }
  },
  entry: {
    'app': './ClientApp/App/app.js',
  },
  output: {
    filename: '[name].js',
    chunkFilename: '[name]-bundle.js',
    path: path.resolve(__dirname, 'wwwroot/app'),
    publicPath: '/app/'
  },

  mode: process.env.NODE_ENV,

  module: {
    rules: [
      { test: /\.js$/, use: 'babel-loader' },
      { test: /\.vue$/, use: 'vue-loader' },
      {
        test: /\.(html)$/,
        use: ['file-loader?name=[name]-[hash].[ext]', 'extract-loader', 'html-loader']
      },
      {
        test: /\.css$/,
        use: [
          { loader: 'style-loader' },
          { loader: MiniCssExtractPlugin.loader, options: { module: true } },
          { loader: 'css-loader', options: { import: true } }
        ]
      },
      {
        test: /\.(png|svg|jpg|gif)$/,
        use: ['file-loader']
      },
      {
        test: /\.(woff|woff2|eot|ttf|otf)$/,
        loader: 'url-loader?limit=30000&name=[name]-[hash].[ext]',
        options: {
          publicPath: './'
        }
      }
    ]
  },
  plugins: [
    new VueLoaderPlugin(),
    new MiniCssExtractPlugin({
      filename: '[name].css',
      chunkFilename: '[name]-bundle.css'
    }),
    //new webpack.ProvidePlugin({
    //    $: "jquery",
    //    jQuery: "jquery"
    //}),
    // Ignore all locale files of moment.js
    new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
    new BundleAnalyzerPlugin()
  ],

  optimization: {
    minimizer: [new TerserPlugin(), new OptimizeCSSAssetsPlugin({})],
    splitChunks: {
      name: 'vendor',
      chunks: 'all'
    }
  }
};