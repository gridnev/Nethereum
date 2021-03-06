using System;
using System.Threading.Tasks;
using EdjCase.JsonRpc.Core;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Hex.HexTypes;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth;

namespace Nethereum.RPC.Personal
{
    /// <Summary>
    ///     personal_unlockAccount
    ///     Unlock an account
    ///     Parameters
    ///     string, address of the account to unlock
    ///     string, passphrase of the account to unlock (optional in console, user will be prompted)
    ///     integer, unlock account for duration seconds (optional)
    ///     Return
    ///     boolean indication if the account was unlocked
    ///     Example
    ///     personal.unlockAccount(eth.coinbase, "mypasswd", 300)
    /// </Summary>
    public class PersonalUnlockAccount : RpcRequestResponseHandler<bool>
    {
        public PersonalUnlockAccount(IClient client) : base(client, ApiMethods.personal_unlockAccount.ToString())
        {
        }

        public async Task<bool> SendRequestAsync(string address, string passPhrase, HexBigInteger durationInSeconds,
            object id = null)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            if (passPhrase == null) throw new ArgumentNullException(nameof(passPhrase));
            if (durationInSeconds == null) throw new ArgumentNullException(nameof(durationInSeconds));
            return await base.SendRequestAsync(id, address.EnsureHexPrefix(), passPhrase, durationInSeconds).ConfigureAwait(false);
        }

        public async Task<bool> SendRequestAsync(EthCoinBase coinbaseRequest, string passPhrase,
            HexBigInteger durationInSeconds,
            object id = null)
        {
            if (coinbaseRequest == null) throw new ArgumentNullException(nameof(coinbaseRequest));
            if (passPhrase == null) throw new ArgumentNullException(nameof(passPhrase));
            return
                await
                    base.SendRequestAsync(id, await coinbaseRequest.SendRequestAsync(), passPhrase, durationInSeconds)
                        .ConfigureAwait(false);
        }

        public RpcRequest BuildRequest(string address, string passPhrase, HexBigInteger durationInSeconds,
            object id = null)
        {
            if (address == null) throw new ArgumentNullException(nameof(address));
            if (passPhrase == null) throw new ArgumentNullException(nameof(passPhrase));
            return base.BuildRequest(id, address.EnsureHexPrefix(), passPhrase, durationInSeconds);
        }
    }
}