import React from 'react';
import 'whatwg-fetch';
import Comment from './Comment';
import CommentList from './CommentList';

class CommentForm extends React.Component {

    constructor(props){
        super(props);
        this.addComment = this.addComment.bind(this);
    }

    addComment(event) {
        var myForm = document.querySelector("#commentForm");
        var formData = new FormData(myForm);
        var actionUrl = myForm.action;
        var comment = new Comment();
        comment.content = formData.get("content");

        actionUrl = "/json/addResult.json";
        fetch(actionUrl).then((response)=> {
            return response.json();
        }).then((addResult)=> {
            this.afterAdd(addResult, comment);
        });
    }

    afterAdd(addResult, comment) {
        if (addResult.success==true){
            this.props.addCommentToList(comment);
        }else {
            console.log("gg");
        }
    }

    render() {
        return (
            <form id="commentForm" action="/Comment/Add/123" method="post">
                <textarea className="form-control" name="content"></textarea>
                <div className="d-flex">
                    <button type="button" className="ml-auto btn btn-outline-primary" onClick={this.addComment}>提交
                    </button>
                </div>
            </form>
        );
    }
}

export default CommentForm;
