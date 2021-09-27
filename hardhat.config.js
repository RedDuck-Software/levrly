require("@nomiclabs/hardhat-waffle");
require("@tenderly/hardhat-tenderly");

// This is a sample Hardhat task. To learn how to create your own go to
// https://hardhat.org/guides/create-task.html
task("accounts", "Prints the list of accounts", async () => {
  const accounts = await ethers.getSigners();

  for (const account of accounts) {
    console.log(account.address);
  }
});

// You need to export an object to set up your config
// Go to https://hardhat.org/config/ to learn more

/**
 * @type import('hardhat/config').HardhatUserConfig
 */
module.exports = {
  tenderly: {
    project: "https://dashboard.tenderly.co/mprokazin/levrly-local",
    username: "michael.prokazin@redduck.io"
  },
  solidity: "0.5.17",
  networks: {
    development: {
      url: "http://127.0.0.1:8545",
    },
    hardhat: {
      chainId: 1,
      throwOnCallFailures: true,
      throwOnTransactionFailures: true,
      // gasPrice: 800000000000,
      forking: {
      	url: "https://eth-mainnet.alchemyapi.io/v2/5VaoQ3iNw3dVPD_PNwd5I69k3vMvdnNj"
      }
    }
  }
};

