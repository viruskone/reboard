var path = require('path');
var ReactRefreshWebpackPlugin = require('@pmmmwh/react-refresh-webpack-plugin');
var HtmlWebpackPlugin = require('html-webpack-plugin');

module.exports = {
    resolve: {
        extensions: ['*', '.js', '.jsx', '.json']
    },
    entry: './src/index.js',
    target: 'web',
    mode: 'development',
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, 'dist'),
        publicPath: '/'
    },
    plugins: [
        new HtmlWebpackPlugin({
            template: 'src/index.ejs',
            minify: {
                removeComments: true,
                collapseWhitespace: true
            },
            inject: true
        }),
        new ReactRefreshWebpackPlugin()
    ],
    module: {
        rules: [
            {
                test: /\.jsx?$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: {
                        presets: [
                            '@babel/preset-env',
                            '@babel/preset-react'
                        ],
                        plugins: ['js-logger', require("react-refresh/babel")]
                    }
                }
            },
            {
                test: /\.css$/i,
                use: ["style-loader", "css-loader"],
            },
            {
                test: /(\.png|jpe?g)/i,
                type: 'asset/resource'
            }
        ]
    },
    devtool: 'eval-source-map',
    devServer: {
        contentBase: path.resolve(__dirname, "dist"),
        historyApiFallback: true,
        port: 3000,
        hot: true
    }
}