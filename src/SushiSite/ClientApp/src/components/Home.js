import React, { Component, useEffect, useState } from "react";

export const Home = () => {
  const [total, setTotal] = useState(0)
  useEffect(() =>{
    fetch('/sushi/total').then(resp => resp.text()).then(res => {
     Number.parseInt()
    })
  },[])


  return (
    <div>
      <h1>Today ordered ${total}</h1>
    </div>
  );
};
