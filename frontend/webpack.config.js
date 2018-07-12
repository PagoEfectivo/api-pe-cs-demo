const path = require('path');
const webpack = require('webpack');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const CopyWebpackPlugin = require('copy-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
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
  entry: ['./src/index.js', './src/style/test.scss'],
  output: {
      path: path.resolve(__dirname, "./"),
      filename: "bundle.js"
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
        test: /\.(css|scss)$/,
        use: ExtractTextPlugin.extract({
          use: [
            'css-loader',
            'sass-loader'
          ],
        })
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
      new ExtractTextPlugin({ 
        filename: 'dist/[name].bundle.css',
        allChunks: true,
      }),
  ]
  .concat(htmlPluginsShared)
  .concat(htmlPlugins)
};