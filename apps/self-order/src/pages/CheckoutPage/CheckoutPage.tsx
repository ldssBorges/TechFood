import { useEffect, useState } from "react";
import {
  Button,
  Flex,
  Heading,
  IconButton,
  Radio,
  Text,
} from "@radix-ui/themes";
import { ArrowLeftIcon } from "lucide-react";
import { QRCodeSVG } from "qrcode.react";
import { useNavigate } from "react-router";
import { t } from "../../i18n";
import { PaymentType } from "../../models";
import { BottomSheet } from "../../components";
import { useOrder } from "../../contexts";

import classNames from "./CheckoutPage.module.css";
import axios from "axios";

const paymentMethods: {
  type: PaymentType;
  name: () => string;
  img: string;
}[] = [
  {
    type: "MERCADOPAGO",
    name: () => t("checkoutPage.mercadoPago"),
    img: "mercado-pago",
  },
  { type: "PIX", name: () => t("checkoutPage.pix"), img: "pix" },
  {
    type: "CREDITCARD",
    name: () => t("checkoutPage.creditCard"),
    img: "credit-card",
  },
];

const PaymentMethod = ({
  name,
  selected,
  img,
  onClick,
}: {
  name: string;
  selected: boolean;
  img: string;
  onClick: () => void;
}) => {
  const src = new URL(`../../assets/payments/${img}.png`, import.meta.url).href;

  return (
    <Flex
      className={classNames.paymentMethod}
      align="center"
      justify="between"
      onClick={onClick}
    >
      <Flex gap="3" align="center">
        <img src={src} />
        <Text>{name}</Text>
      </Flex>
      <Radio value={name} checked={selected} />
    </Flex>
  );
};

const SummaryItem = ({
  name,
  price,
  discount,
}: {
  name: string;
  price: number;
  discount?: boolean;
}) => {
  return (
    <Flex className={classNames.summaryItem} align="center" justify="between">
      <Text color="gray">{name}</Text>
      <Text color={discount ? "green" : undefined} weight="medium">
        {t("labels.currency")}
        {discount ? `-${price.toFixed(2)}` : price.toFixed(2)}
      </Text>
    </Flex>
  );
};

const MercadoPagoPayment = ({
  qrCode,
  total,
  onFinish,
}: {
  qrCode: string;
  total: number;
  onFinish: () => void;
}) => {
  useEffect(() => {}, []);

  const handleFinish = () => {
    onFinish();
  };

  return (
    <BottomSheet showCloseButton={false} closeOnOverlayClick={false}>
      <Flex direction="column" align="center" gap="5">
        <Flex direction="column" align="center">
          <Text size="2" weight="medium">
            {t("labels.total")}
          </Text>
          <Text size="4" weight="bold">
            {t("labels.currency")}
            {total.toFixed(2)}
          </Text>
        </Flex>
        <Text size="4" weight="medium" color="gray">
          {t("checkoutPage.mercadoPagoInstructions")}
        </Text>
        <QRCodeSVG value={qrCode!} size={350} />
        <Button color="grass" onClick={handleFinish}>
          Simulate Payment
        </Button>
      </Flex>
    </BottomSheet>
  );
};

export const CheckoutPage = () => {
  const [isLoading, setIsLoading] = useState(false);
  const [showPayment, setShowPayment] = useState(false);
  const [paymentMethod, setPaymentMethod] = useState(paymentMethods[0].type);

  const navigate = useNavigate();

  const {
    discount,
    total,
    subtotal,
    paymentQrCode,
    createPayment,
    confirmPayment,
  } = useOrder();

  const handlePayment = async () => {
    setIsLoading(true);
    try {
      await createPayment(paymentMethod);
      setShowPayment(true);
    } catch (error) {
      console.error("Error creating payment:", error);
    } finally {
      setIsLoading(false);
    }
  };

  const handleFinish = async () => {
    await confirmPayment();
    navigate("/confirmation", { replace: true });
  };

  return (
    <Flex className={classNames.root} direction="column" align="center" gap="9">
      <Flex
        className={classNames.header}
        align="center"
        justify="between"
        gap="5"
      >
        <IconButton
          variant="outline"
          size="2"
          aria-label="Back"
          onClick={() => navigate(-1)}
        >
          <ArrowLeftIcon size="30" />
        </IconButton>
        <Heading className={classNames.title}>
          {t("checkoutPage.title")}
        </Heading>
      </Flex>
      <Flex className={classNames.content} direction="column" gap="8">
        <Flex className={classNames.paymentMethods} direction="column" gap="4">
          {paymentMethods.map((type) => (
            <PaymentMethod
              key={type.type}
              name={type.name()}
              img={type.img}
              selected={type.type === paymentMethod}
              onClick={() => setPaymentMethod(type.type)}
            />
          ))}
        </Flex>
        <Flex className={classNames.orderSummary} direction="column" gap="4">
          <Heading size="2">{t("checkoutPage.orderSummary")}</Heading>
          <Flex
            className={classNames.orderSummaryContent}
            direction="column"
            gap="2"
          >
            <SummaryItem name={t("checkoutPage.subtotal")} price={subtotal} />
            <SummaryItem
              name={t("checkoutPage.discount")}
              price={discount}
              discount
            />
          </Flex>
          <Flex
            className={classNames.orderSummaryTotal}
            direction="row"
            gap="4"
            justify="between"
          >
            <Text>{t("labels.total")}</Text>
            <Text weight="medium">
              {t("labels.currency")}
              {total.toFixed(2)}
            </Text>
          </Flex>
        </Flex>
        <Button size="3" onClick={handlePayment} disabled={isLoading}>
          {t("checkoutPage.pay")}
        </Button>
      </Flex>
      {showPayment && paymentMethod === "MERCADOPAGO" && (
        <MercadoPagoPayment
          qrCode={paymentQrCode!}
          onFinish={handleFinish}
          total={total}
        />
      )}
    </Flex>
  );
};
