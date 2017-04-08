import React from 'react';
import CommentForm from './CommentForm';
import CommentList from './CommentList';
import Comment from "./Comment";

class CommentBox extends React.Component {

    constructor(){
        super();
        this.addComment = this.addComment.bind(this);
        this.removeComment = this.removeComment.bind(this);
        this.state = {
            comments : [
                new Comment("heheheh"),
                new Comment("xixixixi")
            ]
        };
    }

    addComment(comment) {
        this.setState({
            comments:this.state.comments.concat(comment)
        });
    }

    removeComment(commentId) {
        var comments = this.state.comments.filter(
            comment=>comment.id!=commentId,
        );
        this.setState({comments});
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
