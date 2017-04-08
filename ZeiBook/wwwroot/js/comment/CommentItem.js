import React from 'react';

class CommentItem extends React.Component {

    constructor(props) {
        super(props);
        this.handleRemove = this.handleRemove.bind(this);
    }

    handleRemove(){
        this.props.onRemove(this.props.comment.id);
    }

    render() {
        return (
            <div className="comment-item">
                <div>{this.props.comment.content}</div>
                <div className="d-flex">
                    <div className="ml-auto">
                        <span>{this.props.comment.userName} {this.props.comment.postTime}</span>
                        <span><a href="#" onClick={this.handleRemove}>删除</a></span>
                    </div>
                </div>
            </div>
        );
    }
}

export default CommentItem;
