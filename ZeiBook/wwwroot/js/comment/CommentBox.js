import React from 'react';
import CommentForm from './CommentForm';
import CommentList from './CommentList';
import Comment from "./Comment";
import CommentConfig from './comment-config';
import 'whatwg-fetch';

class CommentBox extends React.Component {

    constructor() {
        super();
        this.addComment = this.addComment.bind(this);
        this.removeComment = this.removeComment.bind(this);

        var config = new CommentConfig();
        this.fetchComments(config.bookId, 1);

        this.state = {
            comments: []
        };
    }

    addComment(comment) {
        this.setState({
            comments: this.state.comments.concat(comment)
        });
    }

    fetchComments(bookId, pageNum) {
        var url = '/Comments/' + bookId + "/p" + pageNum;
        fetch(url).then(function (response) {
            return response.json();
        }).then((json) => {
            if (json.success){
                this.setState({comments: Comment.GetComments(json.comments)});
            }
        });
    }

    removeComment(commentId) {
        var url = '/Comment/Remove/' + commentId;
        url = "/json/addResult.json";
        fetch(url).then(function (response) {
            return response.json();
        }).then((json) => {
            if (json.success) {
                var comments = this.state.comments.filter(
                    comment => comment.id != commentId,
                );
                this.setState({comments});
            }
        });
    }

    render() {
        return (
            <div className="commentBox">
                <CommentForm addCommentToList={this.addComment}/>
                <CommentList comments={this.state.comments} removeComment={this.removeComment}/>
            </div>
        );
    }
}

export default CommentBox;
