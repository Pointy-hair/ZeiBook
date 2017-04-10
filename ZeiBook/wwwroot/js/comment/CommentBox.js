import React from 'react';
import URLSearchParams from 'url-search-params';
import CommentForm from './CommentForm';
import CommentList from './CommentList';
import Comment from "./Comment";
import CommentConfig from './comment-config';

class CommentBox extends React.Component {

    constructor() {
        super();
        this.addComment = this.addComment.bind(this);
        this.removeComment = this.removeComment.bind(this);

        this.config = new CommentConfig();
        this.fetchComments(this.config.bookId, 1);

        this.state = {
            comments: []
        };
    }

    addComment(comment) {
        var url = '/Comments/Add';
        var params = new URLSearchParams();
        params.append('bookId', this.config.bookId);
        params.append('content', comment.content);
        axios.post(url, params, {
            maxRedirects: 0,
            responseType: 'json'
        }).then((response) => {
            var json = response.data;
            if (json.success) {
                this.setState({
                    comments: this.state.comments.concat(comment)
                });
            }
        });

    }

    fetchComments(bookId, pageNum) {
        var url = '/Comments/' + bookId + "/p" + pageNum;
        //url= "/json/commentList.json";
        axios.get(url, {
            responseType: 'json'
        }).then((response) => {
            var data = response.data;
            if (data.success) {
                this.setState({comments: Comment.GetComments(data.comments)});
            }
        });
    }

    removeComment(commentId) {
        var url = '/Comments/Remove/' + commentId;

        axios.post(url, {
            responseType: 'json',
            maxRedirects: 0,
        }).then((response) => {
            var json = response.data;
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
                <CommentList comments={this.state.comments} removeComment={this.removeComment}
                             currentUserId={this.config.userId}/>
            </div>
        );
    }
}

export default CommentBox;
