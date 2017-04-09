import React from 'react';
import CommentItem from './CommentItem';
import '../../css/comment/comment-list.css'

class CommentList extends React.Component {

    constructor(props) {
        super(props);
    }

    getCommentsStr() {
        return this.props.comments.map(comment =>{
            var isCurrentUser=comment.userId===this.props.currentUserId;
            return <CommentItem key={comment.id} onRemove={this.props.removeComment} comment={comment} isCurrentUser={isCurrentUser}/>;
            }
        );
    }

    render() {
        let commentsStr = this.getCommentsStr();

        return (
            <div className="comment-list">
                {commentsStr}
            </div>
        );
    }
}

export default CommentList;
