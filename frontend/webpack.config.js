const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const ExtractTextPlugin = require("extract-text-webpack-plugin");
const CopyWebpackPlugin = require('copy-webpack-plugin');
const extractSass = new ExtractTextPlugin({
  filename: "[name].[contenthash].css",
  disable: process.env.NODE_ENV === "development"
});
const fs = require('fs')

function generateHtmlPlugins (templateDir, nameFile) {
  const templateFiles = fs.readdirSync(path.resolve(__dirname, templateDir + nameFile))
  return templateFiles.map(item => {
    const parts = item.split('.')
    const name = parts[0]
    const extension = parts[1]
    return new HtmlWebpackPlugin({
      filename: `../../source/pagoEfectivo.Api.Demo/Views/${nameFile}/${name}.cshtml`,
      inject: false,
      template: path.resolve(__dirname, `${templateDir}/${nameFile}/${name}.${extension}`)
    })
  })
}

const htmlPluginsShared = generateHtmlPlugins('./src/views/','Shared/')
const htmlPlugins = generateHtmlPlugins('./src/views/','Cips/')

module.exports = {
  entry: require.resolve("./src/style/test.sass"),
  output: {
      path: path.resolve(__dirname, "../output"),
      filename: "bundle.extractText.js"
  },
  watch:true,
  resolve: {
      extensions: ['.js','.ts','.pug','.scss']
  },
  module: {
    rules: [
      {
        test: /\.pug$/,
        loader: 'pug-loader',
        options: {
          pretty: true,
          name: '[name].[ext]'
        }
      },
      {
        test: /\.scss$/,
        use: [
            "style-loader", // creates style nodes from JS strings
            "css-loader", // translates CSS into CommonJS
            "sass-loader" // compiles Sass to CSS
        ]
      }
    ]
  },
  devServer: {
    contentBase: path.join(__dirname, 'dist'),
    compress: true,
    open: true
  },
  plugins: [
      new CopyWebpackPlugin([
        {
          from: 'node_modules/bootstrap/',
          to: '../../source/pagoEfectivo.Api.Demo/wwwroot/lib/bootstrap/'
        },
        {
          from: 'node_modules/jquery/',
          to: '../../source/pagoEfectivo.Api.Demo/wwwroot/lib/jquery/'
        },
        {
          from: 'node_modules/popper.js/',
          to: '../../source/pagoEfectivo.Api.Demo/wwwroot/lib/popper.js/'
        }
      ]),
  ]
  .concat(htmlPluginsShared)
  .concat(htmlPlugins)
};