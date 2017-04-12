var path = require('path');
var webpack = require('webpack');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = {
    entry: './wwwroot/js/entry.js',
    output: {
        filename: 'bundle.js',
        path: path.resolve(__dirname, './wwwroot/js')
    },
    module: {
        loaders: [],
        rules: [
            {
                test: /\.js$/,
                exclude: /(node_modules|bower_components)/,
                loader: 'babel-loader'
            },
            {
                test: /\.css$/,
                use: ExtractTextPlugin.extract({
                    use: 'css-loader'
                })
            }
        ]
    },
    plugins: [
        new ExtractTextPlugin({
            //filename: '../css/[name]-[contenthash].css',
            filename: '../css/[name].css',
            disable: false,
            allChunks: true,
        }),
        new webpack.optimize.UglifyJsPlugin({
            compress: {
                warnings:false
            }
        }),
    ],
    watch: true
};