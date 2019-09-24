using System;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using rafael_becomex_api.Models;
using System.Reflection;

namespace rafael_becomex_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoboController : ControllerBase
    {
        // GET https://localhost:5001/api/robo/status/
        [HttpGet("status")]
        public ActionResult<string> Get()
        {
            RootObject jsonFromFile = readJson();
            return JsonConvert.SerializeObject(jsonFromFile, Formatting.Indented);
        }

        // GET https://localhost:5001/api/robo/status/head/rotate
        [HttpGet("status/head/rotate")]
        public ActionResult<string> GetHeadRotate()
        {
            RootObject jsonFromFile = readJson();
            return jsonFromFile.head.rotate;
        }

        // GET https://localhost:5001/api/robo/status/arm/left/elbow
        [HttpGet("status/arm/left/elbow")]
        public ActionResult<string> GetLeftElbow()
        {
            RootObject jsonFromFile = readJson();
            return jsonFromFile.arm.left.elbow;
        }

        // GET https://localhost:5001/api/robo/status/arm/right/elbow
        [HttpGet("status/arm/right/elbow")]
        public ActionResult<string> GetRightElbow()
        {
            RootObject jsonFromFile = readJson();
            return jsonFromFile.arm.right.elbow;
        }

        // GET https://localhost:5001/api/robo/status/arm/left/wrist
        [HttpGet("status/arm/left/wrist")]
        public ActionResult<string> GetLeftWrist()
        {
            RootObject jsonFromFile = readJson();
            return jsonFromFile.arm.left.wrist;
        }

        // GET https://localhost:5001/api/robo/status/arm/right/wrist
        [HttpGet("status/arm/right/wrist")]
        public ActionResult<string> GetRightWrist()
        {
            RootObject jsonFromFile = readJson();
            return jsonFromFile.arm.right.wrist;
        }

        // POST https://localhost:5001/api/robo/arm/left/elbow/{value}
        [HttpPost("arm/left/elbow/{value}")]
        public object setLeftElbow(int value)
        {
            try
            {
                var list = new List<int> { 1, 2, 3, 4 };
                if (!list.Contains(value))
                {
                    return NotFound("01 - Movimento inexistente");
                }

                RootObject json = readJson();

                if (!(value == Int32.Parse(json.arm.left.elbow) - 1 || value == Int32.Parse(json.arm.left.elbow) + 1))
                {
                    return NotFound("01 - Movimento Inválido");
                }

                json.arm.left.elbow = value.ToString();
                writeJson(json);
                return Ok("movimento efetuado");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // POST https://localhost:5001/api/robo/arm/left/wrist/{value}
        [HttpPost("arm/left/wrist/{value}")]
        public object setLeftWrist(int value)
        {
            try
            {
                var list = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
                if (!list.Contains(value))
                {
                    return NotFound("02 - Movimento inexistente");
                }

                RootObject json = readJson();

                if (!(value == Int32.Parse(json.arm.left.wrist) - 1 || value == Int32.Parse(json.arm.left.wrist) + 1))
                {
                    return NotFound("02 - Movimento Inválido");
                }

                if (json.arm.left.elbow != "4")
                {
                    return NotFound("Só poderá movimentar o Pulso caso o Cotovelo esteja Fortemente Contraído.");
                }

                json.arm.left.wrist = value.ToString();
                writeJson(json);
                return Ok("movimento efetuado");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // POST https://localhost:5001/api/robo/arm/right/elbow/{value}
        [HttpPost("arm/right/elbow/{value}")]
        public object setRightElbow(int value)
        {
            try
            {
                var list = new List<int> { 1, 2, 3, 4 };
                if (!list.Contains(value))
                {
                    return NotFound("03 - Movimento inexistente");
                }

                RootObject json = readJson();

                if (!(value == Int32.Parse(json.arm.right.elbow) - 1 || value == Int32.Parse(json.arm.right.elbow) + 1))
                {
                    return NotFound("03 - Movimento Inválido");
                }
                json.arm.right.elbow = value.ToString();
                writeJson(json);
                return Ok("movimento efetuado");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // POST https://localhost:5001/api/robo/arm/right/wrist/{value}
        [HttpPost("arm/right/wrist/{value}")]
        public object setRightWrist(int value)
        {
            try
            {
                var list = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
                if (!list.Contains(value))
                {
                    return NotFound("04 - Movimento inexistente");
                }

                RootObject json = readJson();

                if (!(value == Int32.Parse(json.arm.right.wrist) - 1 || value == Int32.Parse(json.arm.right.wrist) + 1))
                {
                    return NotFound("04 - Movimento Inválido");
                }

                /*Só poderá movimentar o Pulso caso o Cotovelo esteja Fortemente Contraído. */
                if (json.arm.right.elbow != "4")
                {
                    return NotFound("Só poderá movimentar o Pulso caso o Cotovelo esteja Fortemente Contraído.");
                }

                return Ok("movimento efetuado");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // POST https://localhost:5001/api/robo/head/rotate/{value}
        [HttpPost("head/rotate/{value}")]
        public object setHeadRotate(int value)
        {
            try
            {
                var list = new List<int> { 1, 2, 3, 4, 5 };
                if (!list.Contains(value))
                {
                    return NotFound("05 - Movimento inexistente");
                }

                RootObject json = readJson();

                if (!(value == Int32.Parse(json.head.rotate) - 1 || value == Int32.Parse(json.head.rotate) + 1))
                {
                    return NotFound("05 - Movimento Inválido");
                }

                if (json.head.tilted_head == "3")
                {
                    return NotFound("Só poderá Rotacionar a Cabeça caso sua Inclinação da Cabeça não esteja em estado Para Baixo.");
                }

                json.head.rotate = value.ToString();
                writeJson(json);
                return Ok("movimento efetuado");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        // POST https://localhost:5001/api/robo/head/tilted/{value}
        [HttpPost("head/tilted/{value}")]
        public object setHeadTilted(int value)
        {
            try
            {
                var list = new List<int> { 1, 2, 3 };
                if (!list.Contains(value))
                {
                    return NotFound("06 - Movimento inexistente");
                }

                RootObject json = readJson();

                if (!(value == Int32.Parse(json.head.tilted_head) - 1 || value == Int32.Parse(json.head.tilted_head) + 1))
                {
                    return NotFound("06 - Movimento Inválido");
                }

                json.head.tilted_head = value.ToString();
                writeJson(json);
                return Ok("movimento efetuado");
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private RootObject writeJson(RootObject jsonToEdit)
        {
            try
            {
                var jsonToWrite = JsonConvert.SerializeObject(jsonToEdit, Formatting.Indented);

                using (var writer = new System.IO.StreamWriter("../rafael_becomex_api/robo.json"))
                {
                    writer.Write(jsonToWrite);
                }

                return JsonConvert.DeserializeObject<RootObject>(jsonToWrite);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        private RootObject readJson()
        {
            try
            {
                string jsonFromFile;
                using (var reader = new System.IO.StreamReader("../rafael_becomex_api/robo.json"))
                {
                    jsonFromFile = reader.ReadToEnd();
                }

                return JsonConvert.DeserializeObject<RootObject>(jsonFromFile);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}
