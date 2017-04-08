import React from 'react';
import ReactDom from 'react-dom';
import CommentBox from './comment/CommentBox';

ReactDom.render(
    <CommentBox/>,
    document.querySelector("#commentArea")
);

//doc