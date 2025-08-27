import { useEffect, useState } from "react";
import { Flex, Box, Button } from "@radix-ui/themes";
import clsx from "clsx";

import classNames from "./HomePage.module.css";
import { useNavigate } from "react-router";

const advertImgs = ["1", "2", "3"];
const carouselInterval = 9000;

const Carousel = () => {
  const [index, setIndex] = useState(0);

  const adverts = advertImgs.map((name) => ({
    name,
    src: new URL(`../../assets/advertisements/${name}.png`, import.meta.url)
      .href,
  }));

  useEffect(() => {
    const interval = setInterval(() => {
      setIndex((index + 1) % advertImgs.length);
    }, carouselInterval);

    return () => clearInterval(interval);
  }, [index]);

  return (
    <Box className={classNames.carousel}>
      {adverts.map(({ name, src }, i) => (
        <img
          key={name}
          src={src}
          alt={name}
          className={clsx(
            classNames.carouselImage,
            index === i && classNames.active
          )}
        />
      ))}
    </Box>
  );
};

export const HomePage = () => {
  const navigate = useNavigate();

  const handleStartOrdering = () => {
    navigate("/start");
  };

  return (
    <Flex className={classNames.root} direction="column">
      <Carousel />
      <Flex
        className={classNames.bottom}
        direction="column"
        gap="2"
        align="center"
      >
        <h1>Welcome to the Self-Order</h1>
        <Button
          className={classNames.startButton}
          onClick={handleStartOrdering}
        >
          Start Ordering
        </Button>
      </Flex>
    </Flex>
  );
};
