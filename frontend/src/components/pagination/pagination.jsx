import React from "react";
import { Pagination } from "react-bootstrap";

const MovieListPagination = ({
    currentPage,
    postsPerPage,
    totalPosts,
    paginate
  }) => {
    const pageNumbers = [];
    for (let i = 1; i <= Math.ceil(totalPosts / postsPerPage); i++) {
      pageNumbers.push(i);
    }
    return (
      <>
        <Pagination.Prev  onClick={() => currentPage-1} />
        {pageNumbers.map((number) => (
          <Pagination.Item
            key={number}
            className={`${currentPage === number ? "active" : ""}`}
            onClick={() => {
              paginate(number);
            }}
          >
            {number}
          </Pagination.Item>
        ))}
        <Pagination.Next onClick={() => currentPage+1} />
      </>
    );
  };

export default MovieListPagination;